import React from "react";
import Navbar from "react-bootstrap/Navbar";
import Button from 'react-bootstrap/Button'
import { UtiliseAuth } from "../utils/auth-context";

function NavBar() {

  const auth = UtiliseAuth();

  return (
    <Navbar bg="dark" variant="dark" className="justify-content-between">
      <Navbar.Brand href="/"><h1>Revue par les pairs</h1></Navbar.Brand>
      <Button variant="danger" onClick={auth.onLogout}>Déconnexion</Button>
    </Navbar>
  );
}

export default NavBar;
