import axios from "axios";
import React, { useEffect, useState } from "react";
import { Container } from "react-bootstrap";

import CommentaireAjout from "../commentaire/CommentaireAjout";
import CommentaireRecu from "../commentaire/CommentaireRecu";
import { UtiliseAuth } from "../../utils/auth-context";

function Commentaire({solutionID}) {
  //Passer un props ici pour le path du file
  const [JsonCommentaire, setJsonCommentaire] = useState([]);
  //utiliser le reRender a render quand un commentaire est ecrit. donc prop drilling dans commentaireAjout
  const [reRender, setRender] = useState(false);
  const auth = UtiliseAuth();

  //Ajouter etudiantID pour avoir access seulement au code 
  useEffect(() => {
    axios
      .get("/api/etudiant/commentaires")
      .then(function (response) {
        console.log(response);
        setJsonCommentaire(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  }, []);


  const commentaireAncien = Object.keys(JsonCommentaire);

  return (

    <Container fluid>
      <h2>Commentaires</h2>
      <CommentaireAjout solutionID={solutionID}/>
      {/* {JsonCommentaire?.length &&
      commentaireAncien.map(comment =>{
        return(
          <CommentaireRecu comment={comment}/>
        )
      })} */}
    </Container>
  );
}

export default Commentaire;
