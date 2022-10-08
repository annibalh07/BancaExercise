import "./App.css";
import Layout from "./components/Layout";
import NavBar from "./components/NavBar";
import { Routes, Route } from "react-router-dom";
import Cliente from "./components/Cliente";
import ClienteCuentas from "./components/ClienteCuentas";
import CrearCliente from "./components/CrearCliente";
import CrearCuenta from "./components/CrearCuenta";

function App() {
  return (
    <div>
      <NavBar />
      <Layout>
        <Routes>
          <Route exact path="/clientes" element={<Cliente></Cliente>}></Route>
          <Route
            path="/clientes/:clienteId/cuentas"
            element={<ClienteCuentas></ClienteCuentas>}
          ></Route>
          <Route
            exact
            path="/clientes/crear"
            element={<CrearCliente />}
          ></Route>
          <Route
            exact
            path="/clientes/:identificacion/nueva-cuenta"
            element={<CrearCuenta />}
          ></Route>
          <Route
            exact
            path="/"
            element={
              <div>
                <h2>!Bienvenido al sistema Web del banco!</h2>
                <p>Un banco que realmente es SOLIDARIO CONTIGO</p>
              </div>
            }
          ></Route>
        </Routes>
      </Layout>
    </div>
  );
}

export default App;
