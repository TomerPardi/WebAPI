import React, { useState } from "react";
import { FormControl } from "react-bootstrap";
import InputGroup from "react-bootstrap/InputGroup";
import Button from "react-bootstrap/Button";
import { useContext } from "react";
import AppContext from "../../../AppContext";
import axios from "axios";
import "./Right.css";


export default function MessageInput(props) {
  let sharedContext = useContext(AppContext);
  const [isShown, setIsShown] = useState(false);

  function handleKeyPress(event) {
    if (event.key === "Enter") {
      handleSubmit(event);
    }
  }

  async function handleSubmit(event) {
    event.preventDefault();

    const messageIn = document.getElementById("messageIn");
    if (messageIn.value === "") {
      return;
    }

    // on each time the user submits the form
    // the server insert the message to the messages list with active contact
    try {
      await axios.post(
        `https://${sharedContext.hostname}:7066/api/contacts/${sharedContext.activeContact}/messages`,
        {
          content: messageIn.value,
        },
        { withCredentials: true }
      );
    } catch (err) {
      console.log("error content" + err);
    }

    try {
      await axios.post(
        `https://${props.activeInfo.server}/api/transfer`,
        {
          from: sharedContext.currentUser,
          to: sharedContext.activeContact,
          content: messageIn.value,
        },
        { withCredentials: true }
      );
    } catch (err) {
      console.log(err);
    }

    props.setter(true);
    messageIn.value = "";
  }

  return (
    <div className='wrap-message'>
      <InputGroup
        className='d-flex justify-content-center'
        style={{ flexWrap: "nowrap" }}
        onKeyPress={handleKeyPress}
      >


        {(
          <div className='messageForm'>
            <FormControl
              className='col align-self-stretch'
              placeholder={
                "Enter message here"
              }
              id='messageIn'
              required
            />
          </div>
        )}
        {(
          <Button
            variant='light'
            type='submit'
            onClick={handleSubmit}
            onMouseEnter={() => setIsShown(true)}
            onMouseLeave={() => setIsShown(false)}
          >
            {isShown ? (
              <svg
                xmlns='http://www.w3.org/2000/svg'
                width='16'
                height='16'
                fill='currentColor'
                className='bi bi-send-check-fill'
                viewBox='0 0 16 16'
              >
                <path d='M15.964.686a.5.5 0 0 0-.65-.65L.767 5.855H.766l-.452.18a.5.5 0 0 0-.082.887l.41.26.001.002 4.995 3.178 1.59 2.498C8 14 8 13 8 12.5a4.5 4.5 0 0 1 5.026-4.47L15.964.686Zm-1.833 1.89L6.637 10.07l-.215-.338a.5.5 0 0 0-.154-.154l-.338-.215 7.494-7.494 1.178-.471-.47 1.178Z' />
                <path d='M16 12.5a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Zm-1.993-1.679a.5.5 0 0 0-.686.172l-1.17 1.95-.547-.547a.5.5 0 0 0-.708.708l.774.773a.75.75 0 0 0 1.174-.144l1.335-2.226a.5.5 0 0 0-.172-.686Z' />
              </svg>
            ) : (
              <svg
                xmlns='http://www.w3.org/2000/svg'
                width='16'
                height='16'
                fill='currentColor'
                className='bi bi-send'
                viewBox='0 0 16 16'
              >
                <path d='M15.854.146a.5.5 0 0 1 .11.54l-5.819 14.547a.75.75 0 0 1-1.329.124l-3.178-4.995L.643 7.184a.75.75 0 0 1 .124-1.33L15.314.037a.5.5 0 0 1 .54.11ZM6.636 10.07l2.761 4.338L14.13 2.576 6.636 10.07Zm6.787-8.201L1.591 6.602l4.339 2.76 7.494-7.493Z' />
              </svg>
            )}
          </Button>
        )}
      </InputGroup>
    </div>
  );
}
