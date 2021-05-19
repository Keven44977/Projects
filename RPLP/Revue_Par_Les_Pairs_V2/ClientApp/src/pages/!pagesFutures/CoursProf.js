import React from "react";
import Container from "react-bootstrap/Container";
import Breadcrumb from "react-bootstrap/Breadcrumb";
import Navbar from "../../components/NavBar";
import CardCoursProf from "../../components/cardCours/CardCoursProf";
import { UtiliseAuth } from "../../utils/auth-context";

function CoursProf() {
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
        {listeCours.length ? (
          listeCours.map((cours) => {
            return <CardCoursProf nomCours={cours.nomCours} />;
          })
        ) : (
          <h1>Il n'y a pas de cours de cree</h1>
        )}
      </Container>
    </>
  );
}

export default CoursProf;
