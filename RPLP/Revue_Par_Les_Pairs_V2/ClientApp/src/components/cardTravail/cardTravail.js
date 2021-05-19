import React, { useState, useEffect } from "react";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import ListerSolutions from "./listerSolutions";
import { UtiliseAuth } from "../../utils/auth-context";
import Axios from "axios";
import ModalCommentaires from "./modalCommentaires";
import ModalFinaliser from "../modalFinaliser";

function CardTravail({ travail }) {
  const auth = UtiliseAuth();
  const [afficherDropDown, setAfficherDropDown] = useState(false);
  const [nbCopiesCorrigees, setNbCopiesCorrigees] = useState(0);
  const [modalCommentaires, setModalCommentaires] = useState(false);

  useEffect(() => {
    var route =
      "/api/etudiant/nbcorrections/" +
      travail.travailID +
      "/" +
      auth.user.email;

    Axios.get(route).then((res) => {
      setNbCopiesCorrigees(res.data);
    });
  }, []);

  function AffichageDropDown() {
    if (afficherDropDown) {
      return <ListerSolutions travailID={travail.travailID} />;
    } else {
      return <></>;
    }
  }

  function BoutonDropDown() {
    if (afficherDropDown) {
      return <>⇓</>;
    } else {
      return <>⇐</>;
    }
  }

  return (
    <>
      <Row className="bg-light rounded mb-3">
        <Col>
          <h2>{travail.nom}</h2>
        </Col>
        <Col
          style={{
            display: "flex",
            alignItems: "center",
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
          Copies corrigées: {nbCopiesCorrigees}/{travail.nombresDeRevues}
        </Col>

        <Col
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-end",
          }}
        >
          <Button onClick={() => setModalCommentaires(true)}>
            Voir Commentaires
          </Button>
          <ModalCommentaires
            show={modalCommentaires}
            onHide={() => setModalCommentaires(false)}
          />
        </Col>
        <Col
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-end",
          }}
        >
          <Button onClick={() => setAfficherDropDown(!afficherDropDown)}>
            <BoutonDropDown />
          </Button>
        </Col>
      </Row>
      <AffichageDropDown />
    </>
  );
}

export default CardTravail;
