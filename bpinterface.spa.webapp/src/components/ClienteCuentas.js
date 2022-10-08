import { useEffect, useState } from "react";
import { useMatch } from "react-router-dom";
import axios from "axios";
import DetalleCuentas from "./DetalleCuentas";

const ClienteCuentas = () => {
  const match = useMatch("/clientes/:clienteId/cuentas");
  const { clienteId } = match.params;
  const [clienteNombres, setClienteNombres] = useState("");
  const [clienteCuentas, setClienteCuentas] = useState(null);

  useEffect(() => {
    async function fetchClienteData() {
      try {
        let responseCliente = await axios.get(
          `http://localhost:8080/api/clientes/${clienteId}`
        );
        setClienteNombres(responseCliente.data.nombres);

        let responseClienteCuentas = await axios.get(
          `http://localhost:8080/api/clientes/${responseCliente.data.id}/cuentas`
        );
        setClienteCuentas(responseClienteCuentas.data);
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
    }
    fetchClienteData();
  }, []);

  return (
    <div>
      <h2>
        Cuentas de cliente: <b>{clienteNombres}</b>
      </h2>
      <div>
        <DetalleCuentas cuentas={clienteCuentas} />
      </div>
    </div>
  );
};

export default ClienteCuentas;
