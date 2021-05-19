import React, { useState } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Axios from "axios";

function ModalAjoutTravail(props) {
  const [nom, setNom] = useState("");
  const [dateDeRemise, setDateDeRemise] = useState("");
  const [nombresDeRevues, setNombresDeRevues] = useState("3");
  const [coursId, setCoursId] = useState("1");

  const handleChangeNom = (e) => {
    setNom(e.target.value);
  };

  const handleChangeDateDeRemise = (e) => {
    setDateDeRemise(e.target.value);
  };

  const handleChangeNombreDeRevue = (e) => {
    setNombresDeRevues(e.target.value);
  };

  const handleSubmit = (e) => {
    var jsonObject = {
      Nom: nom,
      DateDeRemise: dateDeRemise,
      NombresDeRevues: nombresDeRevues,
      CoursId: coursId,
    };

    Axios.post("/api/professeur/ajouterTravail", jsonObject).then(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  };

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
          Création d'un nouveau travail
        </Modal.Title>
      </Modal.Header>
      <Form onSubmit={handleSubmit}>
        <Modal.Body>
          <Form.Group>
            <Form.Label>Nom du Travail</Form.Label>
            <Form.Control type="text" onChange={handleChangeNom} />
          </Form.Group>
          <Form.Group>
            <Form.Label>Date de remise</Form.Label>
            <Form.Control type="date" onChange={handleChangeDateDeRemise} />
          </Form.Group>
          <Form.Group>
            <Form.Label>Nombre de revue par étudiant</Form.Label>
            <Form.Control
              type="number"
              placeholder="3"
              min="1"
              onChange={handleChangeNombreDeRevue}
            />
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={props.onHide} variant="danger">
            Annuler
          </Button>
          <Button
            variant="success"
            onClick={() => {
              props.onHide();
            }}
            type="submit"
          >
            Créer
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}

export default ModalAjoutTravail;
