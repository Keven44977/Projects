import React from "react";
import { UtiliseAuth } from "../../utils/auth-context";
import Navbar from '../../components/NavBar';
import { Container } from "react-bootstrap";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import  FileUpload from "../../components/dropzone/FileUpload";

function AjoutSolution() {
  const auth = UtiliseAuth();
  return (
    <>
      <Navbar />
      <Container>
        <Row>
          <Col>
           <FileUpload/>
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default AjoutSolution;