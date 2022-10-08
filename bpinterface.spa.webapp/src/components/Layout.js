const styles = {
  Layout: {
    backgroundColor: "#fff",
    alignItems: "center",
    display: "flex",
    color: "#0A283E",
    flexDirection: "column",
    padding: "20px 0",
  },
  Container: {
    width: "1200px",
  },
};

const Layout = (props) => {
  return (
    <div style={styles.Layout}>
      <div style={styles.Container}>{props.children}</div>
    </div>
  );
};

export default Layout;
