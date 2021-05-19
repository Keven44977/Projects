import React, { Component } from "react";
import { Route, Switch } from "react-router";
import { Layout } from "./layout/Layout";
import "./style/custom.css";

import Home from "./pages/Home";
import Page404 from "./pages/Page404";

// import HomeProfesseur from "./pages/professeur/HomeProfesseur";
import AjoutSolution from "./pages/professeur/AjoutSolution";
import TravauxProfesseur from "./pages/professeur/TravauxProfesseur";
import RecuperationCommentaire from "./pages/professeur/RecuperationCommentaire";

// import CoursEtudiant from "./pages/etudiant/CoursEtudiant";
import TravauxEtudiant from "./pages/etudiant/TravauxEtudiant";
import RevueEtudiant from "./pages/etudiant/RevueEtudiant";

// import the library
import { library } from "@fortawesome/fontawesome-svg-core";

// import your icons
import { far } from "@fortawesome/free-regular-svg-icons";

library.add(far);

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Switch>
          <Route exact path="/" component={Home} />

          {/* <Route exact path="/Prof" component={HomeProfesseur} /> */}
          <Route exact path="/Prof/Solution" component={AjoutSolution} />
          <Route exact path="/Prof/Travaux" component={TravauxProfesseur} />
          <Route
            exact
            path="/Prof/Telechargement"
            component={RecuperationCommentaire}
          />

          {/* <Route exact path="/Etudiant" component={CoursEtudiant} /> */}
          <Route exact path="/Etudiant/Travaux" component={TravauxEtudiant} />
          <Route
            exact
            path="/Etudiant/Travaux/Revue/:solutionID"
            component={RevueEtudiant}
          />

          <Route component={Page404} />
        </Switch>
      </Layout>
    );
  }
}
