import React, { useState } from "react";
import Form from "react-bootstrap/Form";
import { Button, Card, Col } from "react-bootstrap";
import { UtiliseAuth } from "../../utils/auth-context";
import axios from "axios";

import "../commentaire/cardCommentaire.css";

function CommentaireAjout({solutionID}) {
  const [commentaire, setCommentaire] = useState();
  const [severite, setSeverite] = useState("Vert");
  const auth = UtiliseAuth();

  const handleClick = () => {
    if (commentaire === "") {
      return;
    }
    axios
      .post("/api/commentaires/ajouterCommentaire", {
        texte: commentaire,
        severite: severite,
        etudiantID: auth.user.email,
        solutionID: solutionID
      })
      .then(function (response) {
        console.log(response);
      })
      .then(function (error) {
        console.log(error);
      });

    setCommentaire("");
  };
  const handleSelect = (e) => {
    setSeverite(e.target.value);
  };

  const handleChange = (e) => {
    setCommentaire(e.target.value);
  };

  return (
    <Card className="card vert">
      <Col>
        <Card.Body>
          <Form>
            <Form.Group>
              <div className="input-group mb-3">
                <div className="input-group-prepend">
                  <label className="input-group-text" htmlFor="severite">
                    Sévérité
                  </label>
                </div>
                <select
                  className="custom-select"
                  id="severite"
                  onChange={handleSelect}
                >
                  <option defaultValue="Vert">Vert</option>
                  <option value="Jaune">Jaune</option>
                  <option value="Rouge">Rouge</option>
                </select>
              </div>

              <textarea
                value={commentaire}
                style={{ width: "100%", height: "100%", resize: "none" }}
                onChange={handleChange}
                cols={5}
                rows={5}
                required
              />
            </Form.Group>
          </Form>
          <Button variant="primary" onClick={handleClick}>
            Envoyer
          </Button>
        </Card.Body>
      </Col>
    </Card>
  );
}

export default CommentaireAjout;
