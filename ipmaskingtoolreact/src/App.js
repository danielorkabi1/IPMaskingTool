import {  useRef, useState } from "react";
import "./App.css";
import FileUpload from "./FileUpload";

function App() {
  const [file, setFile] = useState(null);
  const [error, setError] = useState("");
  const [herf, setHerf] = useState("");
  const aNode = useRef();
  async function Submit(e) {
    try {
      e.preventDefault();
      const data = new FormData(e.target);
      if (file) {
        data.append("file", file);
        const response = await fetch(
          "http://localhost:5055/api/IPMasking/upload",
          {
            method: "PUT",
            body: data,
          }
        );
        const blob = await response.blob();
        setHerf(URL.createObjectURL(blob));
        setError('')
      } else return setError("Please Upload File");
    } catch (e) {
      setError("There Is A Problem With The Server");
    }
  }
  const aStyle = {
    display: herf ? "block" : "none",
  };
  return (
    <div className="App">
      <center>
        <form onSubmit={Submit}>
          <h1>Mask Log File</h1>
          <FileUpload cb={setFile} errorCb={setError} />
          <button>Submit</button>
        </form>
        <a ref={aNode} href={herf} style={aStyle} download="maskFile.log">
          download
        </a>
        {error && <div>{error}</div>}
      </center>
    </div>
  );
}

export default App;
