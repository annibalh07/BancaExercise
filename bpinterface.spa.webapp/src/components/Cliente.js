import { useState, useRef } from "react";
import Button from "./Button";
import TextBox from "./TextBox";
import DetalleCliente from "./DetalleCliente";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Cliente = () => {
  const [clienteData, setClienteData] = useState(null);
  const [showResults, setShowResults] = useState(false);
  const navigate = useNavigate();

  const inputRef = useRef();

  const navigateToCrearCliente = () => {
    navigate("/clientes/crear");
  };

  const buscarCliente = async () => {
    if (inputRef.current.value !== "") {
      try {
        let response = await axios.get(
          `http://localhost:8080/api/clientes/${inputRef.current.value}`
        );
        console.log(response.data);
        setClienteData(response.data);
      } catch (err) {
        if (err.response) {
          // The client was given an error response (5xx, 4xx)
          console.log(err.response.data);
          console.log(err.response.status);
          console.log(err.response.headers);
        } else if (err.request) {
          console.log(err.request);
        } else {
          console.log("Error", err.message);
        }
      }
      setShowResults(true);
    }
  };

  const handleKeyPress = (event) => {
    if (event.key === "Enter") {
      buscarCliente();
    }
  };
  return (
    <div>
      <h2>Buscar Cliente</h2>
      <div>
        <TextBox
          onKeyPress={handleKeyPress}
          inputref={inputRef}
          placeholder="cédula"
          maxLength="13"
        ></TextBox>
        <Button onClick={buscarCliente}>Buscar</Button> <span> </span>
        <Button onClick={navigateToCrearCliente}>Agregar Cliente</Button>
      </div>

      <div>{showResults ? <DetalleCliente cliente={clienteData} /> : null}</div>
    </div>
  );
};

export default Cliente;
