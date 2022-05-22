import React from "react";
import "./Right.css";

const Chathead = (props) => {
  // const contactData = context.userData; ??????
  // const photo = contactData[props.activeContact].photo; ??????
  if (props.activeContact === "none") {
    return <div>placeholder</div>;
  } else {
    return (
      <div className='chat-head'>
        <img src='default.png' />
        <div className='chat-name'>
          <h1 className='font-name'>{props.activeContact.name}</h1>
        </div>
      </div>
    );
  }
};

export default Chathead;
