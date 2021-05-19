import React, { useState } from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Axios from "axios";
import RecuperationCommentaire from "../../pages/professeur/RecuperationCommentaire";

function ModalVoirStatistiques(props) {
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
          Statistiques pour le travail {props.compName}
        </Modal.Title>
      </Modal.Header>
      <Form>
        <Modal.Body>
          <RecuperationCommentaire travailID={props.travailID} />
        </Modal.Body>
        <Modal.Footer></Modal.Footer>
      </Form>
    </Modal>
  );
}

export default ModalVoirStatistiques;
