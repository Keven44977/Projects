import React, { useState, useEffect } from "react";
import { UtiliseAuth } from "../../utils/auth-context";
import Axios from "axios";
import Solution from "./solution";

export default function ListerSolutions({ travailID }) {
  const auth = UtiliseAuth();
  const [listeSolutions, setlisteSolutions] = useState([]);
  var compteurRevue = 0;

  useEffect(() => {
    var routeApi =
      "/api/etudiant/solution/" + auth.user.email + "/" + travailID;
    Axios.get(routeApi).then((res) => {
      setlisteSolutions(res.data);
    });
  }, []);

  function Affichage() {
    if (listeSolutions.length == 0) {
      return <h4>Aucune solution de disponible pour le moment...</h4>;
    } else {
      return (
        <>
          {listeSolutions.map((solution) => {
            compteurRevue++
            return <Solution solutionID={solution.solutionID} numeroRevue={compteurRevue} />;
          })}
        </>
      );
    }
  }

  return <Affichage />;
}
