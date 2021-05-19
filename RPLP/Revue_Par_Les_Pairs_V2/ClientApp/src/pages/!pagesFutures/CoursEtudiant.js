import React from "react";
import Container from "react-bootstrap/Container";
import Breadcrumb from "react-bootstrap/Breadcrumb";
import Navbar from "../../components/NavBar";
import CardCours from "../../components/cardCours/CardCours";
import { UtiliseAuth } from "../../utils/auth-context";

function CoursEtudiant() {
  const auth = UtiliseAuth();

  var listeCours = [
    { nomCours: "Introduction à la programmation", nbTravail: 1 },
    { nomCours: "Programmation Web", nbTravail: 2 },
    { nomCours: "Réseau", nbTravail: 0 },
  ];

  return (
    <>
      <Navbar />
      <Breadcrumb>
        <Breadcrumb.Item active>{auth.user.displayName}</Breadcrumb.Item>
      </Breadcrumb>
      <Container>
        {listeCours.map((cours) => {
          return (
            <CardCours
              nomCours={cours.nomCours}
              nbTravail={cours.nbTravail}
              redirect={"/Etudiant/Travaux"}
            />
          );
        })}
      </Container>
    </>
  );
}

export default CoursEtudiant;
