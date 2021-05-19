import React, { useEffect, useState } from "react";
import Breadcrumb from "react-bootstrap/Breadcrumb";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import Button from "react-bootstrap/Button";

import { UtiliseAuth } from "../../utils/auth-context";
import Navbar from "../../components/NavBar";
import PrismCode from "../../components/highlightCode/PrismCode";
import Commentaire from "../../components/commentaire/Commentaire";
import Treeview from "../../components/treeview/treeview";
import { useParams } from "react-router-dom";
import { Link } from "react-router-dom";
import ModalFinaliser from "../../components/modalFinaliser";

function RevueEtudiant() {
  const test = "https://localhost:5001";
  const auth = UtiliseAuth();

  const [texteFichier, setTexteFichier] = useState("");
  const [pathFichier, setPathFichier] = useState("");
  const [nomFichier, setNomFichier] = useState("Correction");
  const { solutionID } = useParams();
  const [modalFinaliser, setModalFinaliser] = useState(false);

  function handleClick(node) {
    if (node.Extension !== "") {
      var path = node.Path.match(/(?=\\temp).+(?=)/g, -1);
      var pathFinal = test.concat("", path);
      setPathFichier(pathFinal);
      setNomFichier(node.Nom);
      console.log(pathFinal);
    }
  }

  useEffect(() => {
    if (pathFichier == "") {
      return;
    }
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", pathFichier, false);
    rawFile.onreadystatechange = function () {
      if (rawFile.readyState === 4) {
        if (rawFile.status === 200 || rawFile.status == 0) {
          var allText = rawFile.responseText;
          setTexteFichier(allText);
        }
      }
    };
    rawFile.send(null);
  }, [pathFichier]);

  return (
    <>
      <Navbar />
      <Breadcrumb>
        <Breadcrumb.Item href="/Etudiant/Travaux">
          {auth.user.displayName}
        </Breadcrumb.Item>
        <Breadcrumb.Item active>Revue</Breadcrumb.Item>
      </Breadcrumb>
      <Row>
        <Col style={{ maxWidth: "25%" }}>
          <Row>
            <Col>
              {" "}
              <h2>Fichiers</h2>
            </Col>
          </Row>
          <Row
            style={{
              paddingRight: "0",
              maxHeight: "45em",
              minHeight: "45em",
              overflow: "scroll",
            }}
          >
            <Col>
              {" "}
              <Treeview
                handleClick={handleClick}
                solutionID={solutionID}
                user={auth.user.email}
              />
            </Col>
          </Row>
        </Col>
        <Col style={{ fontSize: "smaller", maxWidth: "50%" }}>
          <h2>{nomFichier}</h2>
          <PrismCode
            code={texteFichier}
            language="clike"
            plugins={["line-numbers"]}
          />
        </Col>
        <Col style={{ paddingLeft: "0", maxWidth: "25%" }}>
          <Commentaire solutionID={solutionID}/>
          <br />
          <br />
          <Link to="/Etudiant/Travaux">
            <Button
              style={{ float: "right", marginRight: "1em" }}
              variant="warning"
            >
              Retour aux travaux
            </Button>
          </Link>
          <Button
            style={{ marginLeft: "1em" }}
            variant="success"
            onClick={() => setModalFinaliser(true)}
          >
            Finaliser
          </Button>
          <ModalFinaliser
            show={modalFinaliser}
            onHide={() => setModalFinaliser(false)}
            solutionID = {solutionID}
            etudiantID = {auth.user.email}
          />
        </Col>
      </Row>
    </>
  );
}

export default RevueEtudiant;
