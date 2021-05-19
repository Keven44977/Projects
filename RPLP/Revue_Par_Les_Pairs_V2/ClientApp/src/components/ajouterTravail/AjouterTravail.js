import React, { useState } from "react";
import Button from "react-bootstrap/Button";
import ModalAjoutTravail from "./ModalAjoutTravail"

function AjouterTravail() {
  const [modalShow, setModalShow] = useState(false);

  return (
    <>
      <Button variant="primary" onClick={() => setModalShow(true)}>
        Ajouter Travail
      </Button>

      <ModalAjoutTravail show={modalShow} onHide={() => setModalShow(false)}/>
    </>
  );
}

export default AjouterTravail;
