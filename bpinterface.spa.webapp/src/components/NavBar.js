import Logo from "./Logo";
import Menu from "./Menu";

const styles = {
  navbar: {
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    height: "100px",
    justifyContent: "space-between",
    padding: "0 50px",
    position: "relative",
    boxShadow: "0 2px 3px rgb(0,0,0,0.1)",
  },
};

const NavBar = () => {
  return (
    <nav style={styles.navbar}>
      <Logo />
      <Menu />
    </nav>
  );
};

export default NavBar;
