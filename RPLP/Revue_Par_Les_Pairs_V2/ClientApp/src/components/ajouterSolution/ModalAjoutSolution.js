import React, { useState } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import axios from "axios";

function ModalAjoutSolution(props) {
  const [file, setFile] = useState();
  const [fileName, setFileName] = useState();

  const saveFile = (e) => {
    console.log(e.target.files[0]);
    setFile(e.target.files[0]);
    setFileName(e.target.files[0].fileName);
  };

  const uploadFile = async (e) => {
    console.log(file);
    const formData = new FormData();
    formData.append("formFile", file);
    formData.append("fileName", fileName);
    formData.append("TravailID", props.travailId);
    try {
      const res = await axios.post(
        "/api/professeur/ajouterSolution",
        formData,
        {
          responseType: "arraybuffer",
          headers: {
            "Content-Disposition": "multipart/form-data",
          },
        }
      );
      console.log(res);
    } catch (ex) {
      console.log(ex);
    }
  };
  console.log(file);

  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      backdrop="static"
      keyboard={false}
      centered
    >
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          Ajout d'une nouvelle solution
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <input type="file" onChange={saveFile} />

        {file != undefined && (
          <Button
            onClick={() => {
              uploadFile();
              props.onHide();
            }}
          >
            {" "}
            Upload{" "}
          </Button>
        )}
      </Modal.Body>
      <Modal.Footer></Modal.Footer>
    </Modal>
  );
}

export default ModalAjoutSolution;
