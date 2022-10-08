const TextBox = (props) => {
  const styles = {
    Input: {
      padding: "12px 20px",
      margin: "8px 0",
      display: "inline-block",
      border: "1px solid #ccc",
      borderRadius: "4px",
      boxSizing: "border-box",
    },
  };
  return <input ref={props.inputref} style={styles.Input} {...props} />;
};

export default TextBox;
