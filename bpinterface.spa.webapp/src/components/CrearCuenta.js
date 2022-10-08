import axios from "axios";
import React, { useEffect, useRef, useState } from "react";
import { Link, useMatch } from "react-router-dom";
import { Button, Dropdown, Form } from "semantic-ui-react";

const CrearCuenta = () => {
  const match = useMatch("/clientes/:identificacion/nueva-cuenta");
  const [clienteNombres, setClienteNombres] = useState("");
  const [saldo, setSaldo] = useState(0);

  const { identificacion } = match.params;

  const [selectedTipoCuenta, setTipoCuenta] = useState("");
  const [errors, setErrors] = useState("");
  const [guardado, setGuardado] = useState(false);
  const numeroCuenta = useRef(null);
  const limiteDiario = useRef(null);

  useEffect(() => {
    async function fetchClienteData() {
      try {
        let responseCliente = await axios.get(
          `http://localhost:8080/api/clientes/${identificacion}`
        );
        setClienteNombres(responseCliente.data.nombres);
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
  }, identificacion);

  const submit = () => {
    const data = {
      identificacionCliente: identificacion,
      limiteDiario: limiteDiario.current.value,
      estado: true,
      saldo: saldo,
      tipoCuenta: selectedTipoCuenta,
      numeroCuenta: numeroCuenta.current.value,
    };
    async function crearCuenta() {
      setGuardado(false);
      setErrors("");
      try {
        let responseCliente = await axios.post(
          `http://localhost:8080/api/cuentas`,
          data
        );
        console.log(responseCliente);
        if (responseCliente.status === 201) {
          setGuardado(true);
          setTipoCuenta("");
          setErrors("");
          limiteDiario.current.value = "";
          saldo.current.value = "";
          numeroCuenta.current.value = "";
        }
      } catch (err) {
        if (err.response) {
          // The client was given an error response (5xx, 4xx)
          console.log(err.response.data);
          console.log(err.response.status);
          console.log(err.response.headers);
          if (err.response.status === 400) {
            var errors = Object.keys(err.response.data.errors).map(function (
              key
            ) {
              return err.response.data.errors[key];
            });
            var element = [].concat(...errors.map((ap) => ap));
            setErrors(element.map((item) => <li key={item}>{item}</li>));
          }
        } else if (err.request) {
          console.log(err.request);
        } else {
          console.log("Error", err.message);
        }
      }
    }
    crearCuenta();
  };
  const tipoCuentaOptions = [
    {
      text: "Ahorro",
      value: "Ahorro",
      key: "Ahorro",
    },
    {
      text: "Corriente",
      value: "Corriente",
      key: "Corriente",
    },
  ];
  const styles = {
    errors: {
      color: "red",
      fontWeight: "bold",
    },
    success: {
      color: "green",
      fontWeight: "bold",
    },
    detalleInteres: {
      padding: "20px 10px",
      border: "2px solid",
      borderRadius: "20px",
      margin: "20px 0",
      color: "green",
      fontWeight: "bold",
      display: "flex",
      justifyContent: "flex-end",
      backgroundColor: "cyan",
    },
  };
  return (
    <div>
      <div>
        <h2>Crea nueva cuenta inversión: {clienteNombres}</h2>
        <br />
      </div>
      {errors !== "" ? <ul style={styles.errors}>{errors}</ul> : null}
      <Form>
        <Form.Field>
          <label>Num cuenta</label>
          <input
            ref={numeroCuenta}
            maxLength={10}
            placeholder="numero de cuenta"
          />
        </Form.Field>
        <Form.Field>
          <label>Tipo cuenta</label>
          <Dropdown
            placeholder="Tipo"
            fluid
            selection
            scrolling
            value={selectedTipoCuenta}
            options={tipoCuentaOptions}
            onChange={(e, data) => {
              setTipoCuenta(data.value);
            }}
          />
        </Form.Field>
        <Form.Field>
          <label>Saldo Inicial</label>
          <input
            valor={saldo}
            onChange={(e) => setSaldo(e.target.value)}
            maxLength={10}
            placeholder="valor de apertura"
          />
        </Form.Field>
        <Form.Field>
          <label>Límite diario</label>
          <input
            ref={limiteDiario}
            maxLength={10}
            placeholder="valor de retiro diario"
          />
        </Form.Field>
        <Button type="submit" onClick={submit}>
          Guardar
        </Button>{" "}
        {guardado ? (
          <div>
            {" "}
            <h2 style={styles.success}>Datos Guardados!</h2>{" "}
            <p>
              <Link to={`/clientes/${identificacion}/cuentas`}>
                Regresar cuentas cliente
              </Link>
            </p>
          </div>
        ) : null}
      </Form>
      {saldo > 0 ? (
        <div style={styles.detalleInteres}>
          <div>
            <div>
              <p>Detalle de inversión</p>
            </div>
            <div>
              <h1>3%</h1>
            </div>
            <div>
              <h2>Total: ${Number(saldo) + saldo * 0.03}</h2>
            </div>
          </div>
        </div>
      ) : null}
    </div>
  );
};

export default CrearCuenta;
