import React from "react";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import { UtiliseAuth } from "../utils/auth-context";
import { Redirect } from "react-router-dom";

import BgImage from "../assets/background-home.jpg";
import "../style/Home.css";

function Home() {
  const auth = UtiliseAuth();

  const Redirection = () => {
    if (auth.user.roles == "student")
      return <Redirect to="/Etudiant/Travaux" />;
    if (auth.user.roles == "teacher") return <Redirect to="/prof" />;
  };

  return (
    <>
      {Redirection()}
      <img src={BgImage} alt=""></img>
      <Container className="text-center container-custom">
        <Row className="row-custom">
          <Col>
            <h1 className="text-white">Revue par les pairs</h1>
          </Col>
        </Row>
        <Row>
          <Col>
            <Button variant="success" onClick={auth.onLogin}>
              Connexion
            </Button>
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default Home;
