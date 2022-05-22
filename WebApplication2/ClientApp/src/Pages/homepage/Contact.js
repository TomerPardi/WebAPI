import React from "react";

const Contact = ({ contactName, photo, lastMessage, lastMessageTime }) => {
  const message = () => {
    // TODO: check plaster
    if (!lastMessage) return;

    return lastMessage.length < 30
      ? lastMessage
      : lastMessage.substring(0, 30) + "...";
  };
  
  return (
    <>
      <div className='contact d-flex justify-content-start align-items-center position-relative'>
        {/* TODO: what to do with images??? */}
        <img
          src={"default.jpg"}
        ></img>
        <div className='font-name'>
          <div className='fw-bolder'>{contactName}</div>
          <div>
            <small>
              {" "}
              {/* TODO: API doesnt support this */}
              {/* {isSelf ? "Me: " : ""} */}
              {message()}
            </small>
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
