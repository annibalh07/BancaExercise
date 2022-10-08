import { Table } from "semantic-ui-react";
import { Link } from "react-router-dom";

const DetalleCliente = ({ cliente }) => {
  const styles = {
    detalle: {
      padding: "20px 0",
    },
  };

  if (cliente == null) {
    return <h2>No se encontró resultados</h2>;
  } else {
    return (
      <div style={styles.detalle}>
        <h3>Resultado</h3>
        <Table singleLine>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Identificacion</Table.HeaderCell>
              <Table.HeaderCell>Nombres</Table.HeaderCell>
              <Table.HeaderCell>Direccion</Table.HeaderCell>
              <Table.HeaderCell>Estado</Table.HeaderCell>
              <Table.HeaderCell>Opciones</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            <Table.Row>
              <Table.Cell>{cliente.identificacion}</Table.Cell>
              <Table.Cell>{cliente.nombres}</Table.Cell>
              <Table.Cell>{cliente.direccion}</Table.Cell>
              <Table.Cell>{cliente.estado ? "Activo" : "Inactivo"}</Table.Cell>
              <Table.Cell>
                <Link to={`/clientes/${cliente.identificacion}/cuentas`}>
                  Ver cuentas
                </Link>{" "}
                <span> / </span>
                <Link to={`/clientes/${cliente.identificacion}/nueva-cuenta`}>
                  Crear cuenta
                </Link>
              </Table.Cell>
            </Table.Row>
          </Table.Body>
        </Table>
      </div>
    );
  }
};

export default DetalleCliente;
