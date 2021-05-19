import React from 'react'
import Breadcrumb from "react-bootstrap/Breadcrumb";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import Navbar from "../../components/NavBar";

function HomeProfesseur()
{
  
    return(
        <>
            <Navbar />
            <Breadcrumb >
                <Breadcrumb.Item>
                    {'Professeur'}
                </Breadcrumb.Item>
            </Breadcrumb>
            <Row>
                <Col>
                </Col>
            </Row>
        </>
    )
    
}

export default HomeProfesseur;