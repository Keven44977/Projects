import React from "react";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import { Link } from "react-router-dom";

function Solution({ solutionID, numeroRevue }) {
  var route = "/Etudiant/Travaux/Revue/" + solutionID
  
  return (
    <Row className="border-bottom ">
      <Col>
        <h4>Revue #{numeroRevue}</h4>
      </Col>
      <Col
        style={{
          display: "flex",
          alignItems: "top",
          justifyContent: "flex-end",
        }}
      >
        <Link to={route}>
          <Button size="sm">Corriger</Button>
        </Link>
      </Col>
    </Row>
  );
}

export default Solution;
