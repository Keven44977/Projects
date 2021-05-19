import express from 'express';
import bodyParser from 'body-parser';
import { MongoClient } from 'mongodb';

const app = express();
app.use(bodyParser.json());

const utiliserDB = async (operations, reponse) => {
  try {
    const client = await MongoClient.connect('mongodb://localhost:27017', {useUnifiedTopology: true});
    const db = client.db('liste-de-lecture');

    await operations(db);

    client.close();
  } catch (erreur) {
    reponse.status(500).json({ message: 'Erreur de connexion à la base de données', erreur });
  }
};

app.get('/hello', (requete, reponse) => reponse.send('Hello World!'));

app.get('/api/pieces', async (requete, reponse) => {
  utiliserDB(async (db) => {
    const pieces = await db
      .collection('musiques')
      .find({})
      .sort({ categorie: 1 });

    reponse.status(200).json(pieces);
  }, reponse);
});

app.get('/api/pieces/:id', async (requete, reponse) => {
  utiliserDB(async (db) => {
    const idPiece = requete.params.id;
    
    const pieces = await db.collection('pieces').findOne({ id: idPiece });

    reponse.status(200).json(pieces);
  }, reponse);
});

app.post('/api/pieces/ajouter', async (requete, reponse) => {
  utiliserDB(async (db) => {
    const pieceId = requete.body.id;
    const nomPiece = requete.body.nom;
    const categoriePiece = requete.body.categorie;
    const artistePiece = requete.body.artiste;

    await db.collection('pieces').insertOne({
      id: pieceId,
      nom: nomPiece,
      categorie: categoriePiece,
      artiste: artistePiece,
    });
  }, reponse);
});

app.post('/api/pieces/:id/modifier', async (requete, reponse) => {
  utiliserDB(async (db) => {
    const idPiece = requete.params.id;
    const nouveauNom = requete.body.nom;
    const nouvelleCategorie = requete.body.categorie;
    const nouvelleArtiste = requete.body.artiste;

    await db.collection('pieces').updateOne(
      { idPiece },
      {
        $set: {
          nom: nouveauNom,
          categorie: nouvelleCategorie,
          artiste: nouvelleArtiste,
        },
      }
    );
  }, reponse);
});

app.post('/api/pieces/:id/supprimer', async (requete, reponse) => {
  utiliserDB(async (db) => {
    const idPiece = requete.params.id;
    const pieces = await db.collection('pieces').findOne({ id: idPiece });

    await db.collection('pieces').deleteOne({ pieces });
  }, reponse);
});

app.listen(8000, () => console.log('Écoute le port 8000'));

// import express, { response } from 'express';
// import bodyParser from 'body-parser';
// import Musique from './musique';

// const app = express();
// app.use(bodyParser.json());

// app.get('/api/pieces', async (requete, reponse)=>{

//     Musique.sort((a,b) => {
//         var catA = a.categories.toUpperCase();
//         var catB = b.categories.toUpperCase();

//         if(catA<catB)
//         {
//             return -1;
//         }
//         if(catA>catB)
//         {
//             return 1;
//         }
//         return 0;
//     });
//     reponse.status(200).json(Musique);

// });
// app.get('/api/pieces/:id', async (requete, reponse)=>{
//     const piece = requete.params.id;

//     reponse.status(200).json(Musique[piece]);

// });
// app.post('/api/pieces/ajouter', async (requete, reponse)=>{

//     const {titre,artiste,categories} = requete.body;

//     var lastID = Musique.length;

//     Musique.push({lastID,titre,artiste,categories});

//     reponse.status(200).json();

// });
// app.put('/api/pieces/:id/modifier', async (requete, reponse)=>{

//     const id = requete.params.id;
//     const {titre,artiste,categories} = requete.body;
//     Musique[id].titre = titre;
//     Musique[id].artiste = artiste;
//     Musique[id].categories = categories;

//     reponse.status(200).json(Musique);

// });
// app.delete('/api/pieces:id/supprimer', async (requete, reponse)=>{
//     const id = requete.params.id
//     Musique.splice(id,1);

//     reponse.status(200).json(Musique);
// });

// app.listen(8000, () => console.log('Ecoute le port 8000'));
