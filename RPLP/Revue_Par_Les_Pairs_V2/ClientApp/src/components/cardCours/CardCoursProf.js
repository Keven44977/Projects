import React from "react";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";


function CardCoursProf({ nomCours })
{
    return (
        <Row className="bg-light rounded mb-3">
          <Col>
            <a href={redirect} className="stretched-link" />
            <h2>{nomCours}</h2>
          </Col>
          <Col
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "flex-end",
            }}
          >
            <a href={redirect} className="stretched-link" />
          </Col>
        </Row>
      );

}

export default CardCoursProf;