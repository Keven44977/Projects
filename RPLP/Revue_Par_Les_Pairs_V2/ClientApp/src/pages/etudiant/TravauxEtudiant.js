import React, { useState } from "react";
import Container from "react-bootstrap/Container";
import Breadcrumb from "react-bootstrap/Breadcrumb";
import Navbar from "../../components/NavBar";
import ListerCardTravail from "../../components/cardTravail/listerCardTravail";
import { UtiliseAuth } from "../../utils/auth-context";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";

function TravauxEtudiant() {
  const auth = UtiliseAuth();

  const [rafraichir, setRafraichir] = useState(false);
  return (
    <>
      <Navbar />
      <Breadcrumb>
        <Breadcrumb.Item>{auth.user.displayName}</Breadcrumb.Item>
        <Breadcrumb.Item active>Travaux</Breadcrumb.Item>
      </Breadcrumb>
      <Container>
        <Row>
          <Col style={{ maxHeight: "45em", overflowY: "scroll" }}>
            <ListerCardTravail rafraichir = {rafraichir} />
          </Col>
        </Row>
        <Row>
          <Col>
            <br />
            <Button onClick={() => setRafraichir(!rafraichir)}>
              Rafraichir
            </Button>
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default TravauxEtudiant;
