import React, { useState, useEffect } from "react";
import ListGroup from "react-bootstrap/ListGroup";
import Axios from "axios";
import "./treeview.css";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome"

function Treeview({ handleClick, solutionID, user }) {
  const [treeData, setTreeData] = useState([]);

  useEffect(() => {
    var routeApi = "/api/etudiant/revue/" + user + "/" + solutionID;

    Axios.get(routeApi).then((res) => {
      setTreeData(res.data.EnfantsNodes);
    });
  }, []);

  const Tree = ({ data = [] }) => {
    return (
      <ListGroup variant="flush">
        {data.map((tree) => (
          <TreeNode node={tree} />
        ))}
      </ListGroup>
    );
  };

  function AfficherDossierIcon({ enfantsVisibles }) {
    if (!enfantsVisibles) {
      return <FontAwesomeIcon icon={["far", "folder"]}/>
    } else {
      return <FontAwesomeIcon icon={["far", "folder-open"]}/>
    }
  }

  const TreeNode = ({ node }) => {
    const [enfantsVisibles, setEnfantsVisibles] = useState(true);

    return (
      <ListGroup.Item>
        <div className="d-flex" onClick={(e) => setEnfantsVisibles((v) => !v)}>
          {node.Extension === "" && (
            <AfficherDossierIcon enfantsVisibles={enfantsVisibles} />
          )}

          <div
            className="col d-tree-head"
            onClick={() => {
              handleClick(node);
            }}
          >
            {node.Nom}
          </div>
        </div>

        {enfantsVisibles && (
          <ListGroup variant="flush">
            <Tree data={node.EnfantsNodes} />
          </ListGroup>
        )}
      </ListGroup.Item>
    );
  };

  return <Tree data={treeData} />;
}

export default Treeview;
