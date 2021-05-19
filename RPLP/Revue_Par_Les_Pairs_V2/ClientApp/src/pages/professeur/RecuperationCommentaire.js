import React, { useEffect, useState } from "react";
import Button from "react-bootstrap/Button"

function RecuperationCommentaire({ travailID }) {
    var route = "/commantaires/" + travailID + ".json";
  return (
    <>
        <a href={route} download>
        <Button>Télécharger les commentaires</Button>
      </a>
    </>
  );
}

export default RecuperationCommentaire;
