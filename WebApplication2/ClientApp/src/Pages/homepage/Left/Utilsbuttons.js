import React from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { useContext, useState } from "react";
import AppContext from "../../../AppContext";
import { useNavigate } from "react-router-dom";
import { getByDisplayValue } from "@testing-library/react";
import context from "react-bootstrap/esm/AccordionContext";
import axios from "axios";
import "./Left.css";

const Utilsbuttons = (props) => {
  let navigate = useNavigate();
  const sharedContext = useContext(AppContext);
  const [show, setShow] = useState(false);
  const [contactName, setContactName] = useState("");
  const [contactNick, setContactNick] = useState("");
  const [contactServer, setContactServer] = useState("");
  const [disabled, setDisabled] = useState(true);

  const handleClose = () => {
    setShow(false);
    setContactName("");
    setDisabled(true);
  };
  const handleShow = () => {
    setShow(true);
  };
  const handleAdd = async (e) => {
    e.preventDefault();

    try {
      const res = await axios.post(
        `https://${sharedContext.hostname}:7066/api/contacts`,
        {
          id: contactName,
          name: contactNick,
          server: contactServer,
        },
        { withCredentials: true }
      );

      if (res.status === 201) {
        // contact exists, server added it to contacts list
        // server generated empty messages list
        props.setter(true);
      } else {
        // 1) the contact that client wish to add is already in his list
        // server returns 409 conflict
        if (res.status === 409) {
          alert("Cannot add yourself or existing user in your contacts list!");
          return;
        }
        // 2) the contact that client wish to add is not exist in the system
        // server returns 404
        if (res.status === 404) {
          alert("User doesn't exist!");
          return;
        }
      }
      handleClose();
    } catch (err) {
      alert("Some error occured");
      handleClose();
      console.log(err);
      return;
    }

    try {
      const res = await axios.post(
        `https://${contactServer}/api/invitations/`,
        {
          from: sharedContext.currentUser,
          to: contactName,
          server: `${sharedContext.hostname}:7066`,
        },
        { withCredentials: true }
      );
    } catch (err) {
      console.log(err);
    }
  };
  const handleChangeName = (event) => {
    setContactName(event.target.value);
    if (contactName && contactNick && contactServer) {
      setDisabled(false);
    }
  };

  const handleChangeNick = (event) => {
    setContactNick(event.target.value);
    if (contactName && contactNick && contactServer) {
      setDisabled(false);
    }
  };

  const handleChangeServer = (event) => {
    setContactServer(event.target.value);
    if (contactName && contactNick && contactServer) {
      setDisabled(false);
    }
  };

  const handleKey = (event) => {
    if (event.key === "Enter" && !disabled) {
      handleAdd(event);
    }
  };

  return (
    <div className='wrap-search d-flex justify-content-around'>
      <Button
        className='utilsBtn'
        variant='light'
        onClick={() => {
          fetch(`http://${sharedContext.hostname}:7066/Logout`);
          sharedContext.connection.invoke("Unjoin", sharedContext.currentUser);
          navigate("/", { replace: true });
          sharedContext.activeContact = "none";
          props.setter(true);
        }}
      >
        <i className='bi bi-box-arrow-left'></i>&nbsp; Logout
      </Button>
      <Button className='utilsBtn' variant='light' onClick={handleShow}>
        <i className='bi bi-person-plus'></i>&nbsp; Add Contact
      </Button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add new contact here</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form
            onSubmit={(e) => {
              e.preventDefault();
            }}
          >
            <Form.Group className='mb-3' controlId='newContact'>
              <Form.Label>Contact's ID (Username)</Form.Label>
              <Form.Control
                placeholder="Contact's ID"
                onChange={handleChangeName}
                onKeyDown={handleKey}
                autoFocus
              />
              <Form.Label>Contact's Nickname</Form.Label>
              <Form.Control
                placeholder="Contact's nickname"
                onChange={handleChangeNick}
                onKeyDown={handleKey}
                autoFocus
              />
              <Form.Label>Contact's Server</Form.Label>
              <Form.Control
                placeholder="Contact's server"
                onChange={handleChangeServer}
                onKeyDown={handleKey}
                autoFocus
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant='secondary' onClick={handleClose}>
            Close
          </Button>
          <Button
            id='butt'
            variant='success'
            onClick={(e) => {
              handleAdd(e);
            }}
            disabled={disabled}
          >
            Add Contact
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default Utilsbuttons;
