import React, { useState } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Axios from "axios";

function ModalSupprimerTravail(props) {
  const [decision, setDecision] = useState(false);

  console.log(props.compId)
  
  const handleDecision = (e) => {
    setDecision(e.target.value);
  };

  const handleSubmit = (e) => {
    Axios.delete("/api/professeur/supprimerTravail/" + props.compId).then(
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
      size="sm"
      aria-labelledby="contained-modal-title-vcenter"
      backdrop="static"
      keyboard={false}
      centered
    >
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          Êtes-vous certain de vouloir supprimer le travail {props.compName}
        </Modal.Title>
      </Modal.Header>
      <Form onSubmit={handleSubmit}>
        <Modal.Body></Modal.Body>
        <Modal.Footer>
          <Button onClick={props.onHide} variant="danger">
            Non
          </Button>
          <Button
            variant="success"
            onClick={() => {
              props.onHide();
            }}
            type="submit"
          >
            Oui
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}

export default ModalSupprimerTravail;
