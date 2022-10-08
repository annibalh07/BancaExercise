import axios from "axios";
import React, { useRef, useState } from "react";
import { Button, Dropdown, Form } from "semantic-ui-react";

const CrearCliente = () => {
  const [selectedGender, setGender] = useState("");
  const [errors, setErrors] = useState("");
  const [guardado, setGuardado] = useState(false);
  const identificacion = useRef(null);
  const nombres = useRef(null);
  const edad = useRef(null);
  const direccion = useRef(null);
  const telefono = useRef(null);
  const contrasenia = useRef(null);
  const submit = () => {
    const data = {
      identificacion: identificacion.current.value,
      nombres: nombres.current.value,
      edad: edad.current.value,
      direccion: direccion.current.value,
      telefono: telefono.current.value,
      contrasenia: contrasenia.current.value,
      genero: selectedGender,
    };
    async function crearCliente() {
      setGuardado(false);
      setErrors("");
      try {
        let responseCliente = await axios.post(
          `http://localhost:8080/api/clientes`,
          data
        );
        console.log(responseCliente);
        if (responseCliente.status === 201) {
          setGuardado(true);
          setGender("");
          setErrors("");
          identificacion.current.value = "";
          nombres.current.value = "";
          edad.current.value = "";
          direccion.current.value = "";
          telefono.current.value = "";
          contrasenia.current.value = "";
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
    crearCliente();
  };
  const generoOptions = [
    {
      text: "Masculino",
      value: "Masculino",
      key: "Masculino",
    },
    {
      text: "Femenino",
      value: "Femenino",
      key: "Femenino",
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
  };
  return (
    <div>
      <div>
        <h2>Crea nuevo cliente</h2>
        <br />
      </div>
      {errors !== "" ? <ul style={styles.errors}>{errors}</ul> : null}
      <Form>
        <Form.Field>
          <label>Identificación</label>
          <input ref={identificacion} maxLength={13} placeholder="cédula" />
        </Form.Field>
        <Form.Field>
          <label>Nombers</label>
          <input ref={nombres} placeholder="nombres completos" />
        </Form.Field>
        <Form.Field>
          <label>Género</label>
          <Dropdown
            placeholder="Género"
            fluid
            selection
            scrolling
            value={selectedGender}
            options={generoOptions}
            onChange={(e, data) => {
              setGender(data.value);
            }}
          />
        </Form.Field>
        <Form.Field>
          <label>Edad</label>
          <input ref={edad} maxLength={3} placeholder="edad" />
        </Form.Field>
        <Form.Field>
          <label>Dirección</label>
          <input ref={direccion} maxLength={50} placeholder="dirección" />
        </Form.Field>
        <Form.Field>
          <label>Teléfono</label>
          <input ref={telefono} maxLength={15} placeholder="teléfono celular" />
        </Form.Field>
        <Form.Field>
          <label>Clave</label>
          <input
            ref={contrasenia}
            type="password"
            placeholder="clave de acceso"
          />
        </Form.Field>
        <Button type="submit" onClick={submit}>
          Guardar
        </Button>{" "}
        {guardado ? <h2 style={styles.success}>Datos Guardados!</h2> : null}
      </Form>
    </div>
  );
};

export default CrearCliente;
