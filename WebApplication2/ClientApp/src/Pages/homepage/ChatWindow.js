import React from "react";
import ChatBubble from "./ChatBubble";
import { useRef, useEffect } from "react";

const Chatwindow = (props) => {
  // create an array with the messages, push the default messages
  // TODO: do we really need to insert it to array?
  let arr = [];
  if (props.messages) {
    arr = props.messages;
  }

  const lastMessage = useRef(null);

  const executeScroll = () => lastMessage.current.scrollIntoView();

  useEffect(() => {
    executeScroll();
  }, [props.messages]);

  return (
    <div className='wrap-chat'>
      <div className='chat'>
        {/* turn the array into a map, and pass the message to it, thus rendering the messages from the array */}
        {arr.map((msg) => (
          <ChatBubble key={msg.value} message={msg} />
        ))}
        <div ref={lastMessage}></div>
      </div>
    </div>
  );
};

export default Chatwindow;
