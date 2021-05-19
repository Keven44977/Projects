import React, { useState } from "react";
import Button from "react-bootstrap/Button";
import ModalAjoutSolution from "./ModalAjoutSolution"

function AjouterSolution() {
    const [modalShow, setModalShow] = useState(false);

    return (
        <>
            <Button variant="secondary" onClick={() => setModalShow(true)}>
                Ajouter Solution
      </Button>

            <ModalAjoutSolution show={modalShow} onHide={() => setModalShow(false)} />
        </>
    );
}

export default AjouterSolution;