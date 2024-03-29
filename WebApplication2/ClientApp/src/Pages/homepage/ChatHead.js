import React, { useContext } from "react";
import AppContext from "../../../AppContext";

const Chathead = (props) => {
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
