import React, { useState } from "react";
import Container from "react-bootstrap/Container";
import Breadcrumb from "react-bootstrap/Breadcrumb";
import ListerCardTravauxProf from "../../components/cardTravail/listerCardTravailProf";
import AjouterTravail from "../../components/ajouterTravail/AjouterTravail";
import Navbar from "../../components/NavBar";
import Row from "react-bootstrap/row";
import Col from "react-bootstrap/Col";
import { UtiliseAuth } from "../../utils/auth-context";

function TravauxProfesseur() {
  const auth = UtiliseAuth();
  const [getTravaux, setGetTravaux] = useState(false);

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
            <ListerCardTravauxProf getTravaux={getTravaux}/>
          </Col>
        </Row>
        <br/>
        <Row>
          <Col>
            <AjouterTravail />
          </Col>
        </Row>
      </Container>
    </>
  );
}
export default TravauxProfesseur;
