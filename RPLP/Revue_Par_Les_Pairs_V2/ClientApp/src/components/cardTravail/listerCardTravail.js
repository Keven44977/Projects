import React, { useState, useEffect } from "react";
import { UtiliseAuth } from "../../utils/auth-context";
import CardTravail from "./cardTravail";
import Axios from "axios";

export default function ListerCardTravail({rafraichir}) {
  const auth = UtiliseAuth();
  const [listeTravaux, setListeTravaux] = useState([]);

  useEffect(() => {
    var routeApi = "/api/etudiant/travail/" + auth.user.email;
    Axios.get(routeApi).then((res) => {
      setListeTravaux(res.data);
    });
  }, []);
  
  useEffect(() => {
    var routeApi = "/api/etudiant/travail/" + auth.user.email;
    Axios.get(routeApi).then((res) => {
      setListeTravaux(res.data);
    });
  }, [rafraichir]);

  function Affichage() {
    if (listeTravaux.length == 0) {
      return <h1>Aucun travail de disponible</h1>;
    } else {
      return listeTravaux.map((travail) => {
        return <CardTravail travail={travail} />;
      });
    }
  }

  return (
    <>
      <Affichage />
    </>
  );
}
