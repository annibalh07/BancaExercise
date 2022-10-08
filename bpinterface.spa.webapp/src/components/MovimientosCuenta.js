import axios from "axios";
import { useEffect, useState } from "react";
import { Table } from "semantic-ui-react";

const MovimientosCuenta = ({ numeroCuenta }) => {
  const [movimientos, setMovimientos] = useState(null);
  const styles = {
    detalle: {
      padding: "20px 0",
    },
  };

  useEffect(() => {
    async function fetchClienteData() {
      try {
        let response = await axios.get(
          `http://localhost:8080/api/movimientos/${numeroCuenta}`
        );
        setMovimientos(response.data);
      } catch (err) {
        if (err.response) {
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

  if (movimientos == null || movimientos.length === 0) {
    return <h3>No se encontró movimientos</h3>;
  } else {
    return (
      <div style={styles.detalle}>
        <h2>Listado de movimientos</h2>
        <Table singleLine>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Fecha</Table.HeaderCell>
              <Table.HeaderCell>Número Comprobante</Table.HeaderCell>
              <Table.HeaderCell>Saldo Inicial</Table.HeaderCell>
              <Table.HeaderCell>Movimiento</Table.HeaderCell>
              <Table.HeaderCell>Saldo Disponible</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {movimientos.map((data) => {
              return (
                <Table.Row key={data.numeroComprobante}>
                  <Table.Cell>{data.fecha}</Table.Cell>
                  <Table.Cell>{data.numeroComprobante}</Table.Cell>
                  <Table.Cell>{data.saldoInicial}</Table.Cell>
                  <Table.Cell>{data.movimiento}</Table.Cell>
                  <Table.Cell>{data.saldoDisponible}</Table.Cell>
                </Table.Row>
              );
            })}
          </Table.Body>
        </Table>
      </div>
    );
  }
};

export default MovimientosCuenta;
