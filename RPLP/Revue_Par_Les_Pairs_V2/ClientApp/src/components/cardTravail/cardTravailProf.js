import React, { useState } from "react";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import ModalSupprimerTravail from "../ajouterTravail/ModalSupprimerTravail";
import ModalVoirStatistiques from "../ajouterTravail/ModalVoirStatistiques";
import ModalAjoutSolution from "../ajouterSolution/ModalAjoutSolution";

function CardTravailProf({ travail }) {
  const [modalShowAjoutSolution, setModalShowAjoutSolution] = useState(false);
  const [modalShowSupprimerTravail, setModalShowSupprimerTravail] = useState(
    false
  );
  const [modalShowVoirStatistiques, setModalShowVoirStatistiques] = useState(
    false
  );

  return (
    <>
      <Row className="bg-light rounded mb-3">
        <Col>
        <h2>{travail.nom}</h2>
        </Col>
        <Col
          style={{
            display: "flex",
            alignItems: "top",
            justifyContent: "flex-end",
          }}
        >
          Date de remise: {travail.dateDeRemise.substring(0, 10)}
        </Col>
        <Col
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-end",
          }}
          xs={2}
        >
          <Button
            variant="primary"
            onClick={() => setModalShowAjoutSolution(true)}
          >
            Ajouter solutions
          </Button>
          <ModalAjoutSolution
            show={modalShowAjoutSolution}
            onHide={() => setModalShowAjoutSolution(false)}
            travailId={travail.travailID}
          />
        </Col>
        <Col
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-end",
          }}
          xs={2}
        >
          <Button
            variant="primary"
            onClick={() => setModalShowVoirStatistiques(true)}
          >
            {" "}
            Statistiques
          </Button>
          <ModalVoirStatistiques
            compId={travail.travailID}
            compName={travail.nom}
            show={modalShowVoirStatistiques}
            onHide={() => setModalShowVoirStatistiques(false)}
            travailID = {travail.travailId}
          />
        </Col>
        <Col
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-end",
          }}
          xs={2}
        >
          <Button
            variant="danger"
            onClick={() => setModalShowSupprimerTravail(true)}
          >
            {" "}
            <b>X</b>
          </Button>

          <ModalSupprimerTravail
            compId={travail.travailID}
            compName={travail.nom}
            show={modalShowSupprimerTravail}
            onHide={() => setModalShowSupprimerTravail(false)}
          />
        </Col>
      </Row>
    </>
  );
}

export default CardTravailProf;
