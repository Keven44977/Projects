import React from "react";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

function CardCours({ nomCours, nbTravail, redirect }) {
  function AffichageNouveauTravail() {
    if (nbTravail === 0) {
      return "aucun nouveau travail!";
    } else if (nbTravail === 1) {
      return "1 nouveau travail!";
    } else {
      return nbTravail + " nouveaux travaux!";
    }
  }
  
  return (
    <Row className="bg-light rounded mb-3">
      <Col>
        <a href={redirect} className="stretched-link" />
        <h2>{nomCours}</h2>
      </Col>
      <Col
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "flex-end",
        }}
      >
        <a href={redirect} className="stretched-link" />
        <a>{AffichageNouveauTravail()}</a>
      </Col>
    </Row>
  );
}

export default CardCours;
