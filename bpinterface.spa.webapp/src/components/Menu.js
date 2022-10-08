import { Link } from "react-router-dom";

const Menu = () => {
  const styles = {
    MenuItem: {
      listStyleType: "none",
      display: "flex",
    },
    Li: {
      padding: "20px",
    },
    A: {
      textDecoration: "none",
      cursor: "pointer",
      color: "blue",
    },
  };

  return (
    <nav>
      <ul style={styles.MenuItem}>
        <li style={styles.Li}>
          <Link style={styles.A} to="/">
            Inicio
          </Link>
        </li>
        <li style={styles.Li}>
          <Link style={styles.A} to="/clientes">
            Clientes
          </Link>
        </li>
      </ul>
    </nav>
  );
};

export default Menu;
