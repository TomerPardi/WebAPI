import React from "react";
import "./Left.css";

const Contact = ({ contactName, photo, lastMessage, lastMessageTime }) => {
  const message = () => {
    if (!lastMessage) return;

    return lastMessage.length < 30
      ? lastMessage
      : lastMessage.substring(0, 30) + "...";
  };

  return (
    <>
      <div className='contact d-flex justify-content-start align-items-center position-relative'>
        <img src={"default.png"}></img>
        <div className='font-name'>
          <div className='fw-bolder'>{contactName}</div>
          <div>
            <small> {message()}</small>
          </div>
        </div>
        <p id='contact-time' className='text-muted align-self-start'>
          <small>{lastMessage ? lastMessageTime : ""}</small>
        </p>
      </div>
    </>
  );
};

export default Contact;
