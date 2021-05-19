import React, { useState, useCallback } from "react";
import axios from "axios";
import { Button } from "react-bootstrap";
import { useDropzone } from "react-dropzone";

function FileUpload(props) {
  const [file, setFile] = useState();
  const [fileName, setFileName] = useState();
  //CoursID represente le cours lorsque le professeur click sur un cours, donc le cours va matcher pour pouvoir creer la solution et l'introduire dans la bd
  
  const saveFile = (e) => {
    console.log(e.target.files[0]);
    setFile(e.target.files[0]);
    setFileName(e.target.files[0].fileName);
  };

  const uploadFile = async (e) => {
    console.log(file);
    const formData = new FormData();
    formData.append("formFile", file);
    formData.append("fileName", fileName);
    formData.append("TravailID", props.travailId);
    try {
      const res = await axios.post(
        "/api/professeur/ajouterSolution",
        formData,
        {
          responseType: "arraybuffer",
          headers: {
            "Content-Disposition": "multipart/form-data",
          },
        }
      );
      console.log(res);
    } catch (ex) {
      console.log(ex);
    }
  };
  console.log(file);
  return (
    <>
      <input type="file" onChange={saveFile} />
      {file != undefined && <Button onClick={uploadFile}> Téléverser </Button>}
    </>
  );
}

export default FileUpload;
