import React from "react";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button"

function ModalCommentaires(props)
{
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
              <h2>Commentaires</h2>
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            Aucun commentaires de disponible pour le moment...
          </Modal.Body>
          <Modal.Footer>
            <Button onClick={props.onHide} variant="danger">
             Fermer
            </Button>
          </Modal.Footer>
        </Modal>
      );
}

export default ModalCommentaires