import React from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import Axios from "axios";
import {Link} from "react-router-dom";

function ModalFinaliser(props) {
  function FinaliserCorrection() {
    var routeApi =
      "/api/etudiant/travail/finaliser/" +
      props.solutionID +
      "/" +
      props.etudiantID;
    Axios.put(routeApi);
  }
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
          Voulez-vous confirmer la remise?
        </Modal.Title>
      </Modal.Header>
      <Modal.Footer>
        <Button onClick={props.onHide} variant="danger">
          Non
        </Button>
        <Link to="/Etudiant/Travaux">
          <Button
            variant="success"
            onClick={() => {
              props.onHide();
              FinaliserCorrection();
            }}
          >
            Oui
          </Button>
        </Link>
      </Modal.Footer>
    </Modal>
  );
}

export default ModalFinaliser;
