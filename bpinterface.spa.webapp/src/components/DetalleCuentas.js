import { Table } from "semantic-ui-react";
import Button from "./Button";
import { Link } from "react-router-dom";
import { useState } from "react";
import MovimientosCuenta from "./MovimientosCuenta";

const DetalleCuentas = ({ cuentas }) => {
  const [numeroCuenta, setNumeroCuenta] = useState("");
  const styles = {
    detalle: {
      padding: "20px 0",
    },
  };
  const onClickHandler = (numeroCuenta) => {
    //alert(numeroCuenta);
    setNumeroCuenta(numeroCuenta);
  };

  if (cuentas == null) {
    return <h3>No se encontró cuentas asociadas</h3>;
  } else {
    return (
      <div style={styles.detalle}>
        <Table singleLine>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Número Cuenta</Table.HeaderCell>
              <Table.HeaderCell>Tipo Cuenta</Table.HeaderCell>
              <Table.HeaderCell>Saldo Inicial</Table.HeaderCell>
              <Table.HeaderCell>Limite Diario</Table.HeaderCell>
              <Table.HeaderCell>Movimientos</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {cuentas.map((data) => {
              return (
                <Table.Row key={data.numeroCuenta}>
                  <Table.Cell>{data.numeroCuenta}</Table.Cell>
                  <Table.Cell>{data.tipoCuenta}</Table.Cell>
                  <Table.Cell>{data.saldoInicial}</Table.Cell>
                  <Table.Cell>{data.limiteDiario}</Table.Cell>
                  <Table.Cell>
                    <Button onClick={() => onClickHandler(data.numeroCuenta)}>
                      Ver
                    </Button>
                  </Table.Cell>
                </Table.Row>
              );
            })}
          </Table.Body>
        </Table>
        <Link to="/cuentas/crear">Agregar cuenta</Link>
        <div>
          {numeroCuenta !== "" ? (
            <MovimientosCuenta numeroCuenta={numeroCuenta} />
          ) : null}
        </div>
      </div>
    );
  }
};

export default DetalleCuentas;
