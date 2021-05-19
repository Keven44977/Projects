import os

import numpy as np
from numpy import save
from numpy import load

from tensorflow.keras.preprocessing.image import ImageDataGenerator
from tensorflow.keras.applications import MobileNetV2
from tensorflow.keras.layers import AveragePooling2D
from tensorflow.keras.layers import Dropout
from tensorflow.keras.layers import Flatten
from tensorflow.keras.layers import Dense
from tensorflow.keras.layers import Input
from tensorflow.keras.models import Model
from tensorflow.keras.optimizers import Adam
from tensorflow.keras.preprocessing.image import img_to_array
from tensorflow.keras.preprocessing.image import load_img
from tensorflow.keras.applications.mobilenet_v2 import preprocess_input
from tensorflow.keras.utils import to_categorical

from sklearn.preprocessing import LabelBinarizer
from sklearn.model_selection import train_test_split
from sklearn.metrics import classification_report

import matplotlib.pyplot as plt


learningRate = 1e-4
epochs = 20
batchSize = 32

directory = "dataset/"
categories = ["with_mask", "no_mask"]


def load_images():
    data = []
    labels = []

    for category in categories:
        path = os.path.join(directory, category)
        for image in os.listdir(path):
            print(len(labels))
            imagePath = os.path.join(path, image)
            image = load_img(imagePath, target_size=(224, 224))
            image = img_to_array(image)
            image = preprocess_input(image)

            data.append(image)
            labels.append(category)

    save("data.npy", data)
    save("labels.npy", labels)


def train_model():
    data = load("data.npy")
    labels = load("labels.npy")

    lb = LabelBinarizer()
    labels = lb.fit_transform(labels)
    labels = to_categorical(labels)

    data = np.array(data, dtype="float32")
    labels = np.array(labels)

    (trainX, testX, trainY, testY) = train_test_split(data, labels, test_size=0.20, stratify=labels, random_state=42)

    aug = ImageDataGenerator(
        rotation_range=20,
        zoom_range=0.15,
        width_shift_range=0.2,
        height_shift_range=0.2,
        shear_range=0.15,
        horizontal_flip=True,
        fill_mode="nearest"
    )

    baseModel = MobileNetV2(weights="imagenet", include_top=False, input_tensor=Input(shape=(224, 224, 3)))

    headModel = baseModel.output
    headModel = AveragePooling2D(pool_size=(7, 7))(headModel)
    headModel = Flatten(name="flatten")(headModel)
    headModel = Dense(128, activation="relu")(headModel)
    headModel = Dropout(0.5)(headModel)
    headModel = Dense(2, activation="softmax")(headModel)

    model = Model(inputs=baseModel.inputs, outputs=headModel)

    for layer in baseModel.layers:
        layer.trainable = False

    opt = Adam(learning_rate=learningRate, decay=learningRate / epochs)
    model.compile(loss="binary_crossentropy", optimizer=opt, metrics=["accuracy"])

    H = model.fit(aug.flow(trainX, trainY, batch_size=batchSize),
                  steps_per_epoch=len(trainX) // batchSize,
                  validation_data=(testX, testY),
                  validation_steps=len(testX) // batchSize,
                  epochs=epochs)

    predIdxs = model.predict(testX, batch_size=batchSize)
    predIdxs = np.argmax(predIdxs, axis=1)

    print(classification_report(testY.argmax(axis=1), predIdxs, target_names=lb.classes_))

    model.save("mask_detector.model", save_format="h5")


train_model()
