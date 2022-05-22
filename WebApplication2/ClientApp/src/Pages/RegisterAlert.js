import { Button, Modal } from "react-bootstrap";
import { useState } from "react";
import React from "react";

export default function RegisterAlert(props) {
  const [show, setShow] = useState(props.show);

  const handleClose = () => {
    setShow(false);
    props.navigate("/", { replace: true });
  };


  return (
    <>
      <Modal variant='success' show={show} onHide={handleClose}>
        <Modal.Header>
          <Modal.Title>Success!</Modal.Title>
        </Modal.Header>
        <Modal.Body>Registration successful, Proceed to Login page</Modal.Body>
        <Modal.Footer>
          <Button variant='secondary' onClick={handleClose}>
            Proceed
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
