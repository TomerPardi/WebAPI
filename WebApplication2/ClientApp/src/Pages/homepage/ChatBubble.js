import React from "react";
import { useState } from "react";
import AppContext from "../../AppContext";


export default function ChatBubble(props) {
  // JSON object - {id, content, created, sent}
  const { id, content, created, sent, sender } = props.message;
  let sharedContext = React.useContext(AppContext);
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const convertTime = (toConvert) => {
    if (toConvert) {
      const formatted = new Date(toConvert)
      return (formatted.getHours() + ':' + ("0" + formatted.getMinutes()).slice(-2));
    }
  }

  return (
    // TODO: check whether "sent" is false in case i sent the message
    <div className={`chat-bubble ${sender === sharedContext.currentUser ? "me" : "you"}`}>
      {content}
      <h6 className='text-muted' style={{ justifySelf: "right" }}>
        {convertTime(created)}
      </h6>
    </div>
  );
}
