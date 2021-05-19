import React, { useState, useEffect } from "react";
import Axios from "axios";
import CardTravailProf from "./cardTravailProf";

function ListerCardTravailProf(props) {
  const [listeTravaux, setListeTravaux] = useState([]);

  useEffect(() => {
    Axios.get("/api/professeur/envoyerTravaux")
      .then((response) => {
        setListeTravaux(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, [props.getTravaux]);

  function Affichage()
  {
    if (listeTravaux.length == 0) {
      return <h1>Aucun travail de disponible</h1>;
    } else {
      return listeTravaux.map((travail) => {
        return <CardTravailProf travail={travail} />;
      });
    }
  }

  return (
    <>
      <Affichage />
    </>
  );
}

export default ListerCardTravailProf;
