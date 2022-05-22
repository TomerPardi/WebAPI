import React from "react";
import Image from "react-bootstrap/Image";
import { Card } from "react-bootstrap";

function MessageBox({
  messageText,
  Sender,
  time,
  photo = require("./default.jpg"),
}) {
  /* Pass arguments when calling message box to print them out */
  return (
    <Card className='shadow rounded mb-3 '>
      <div className='row g-1'>
        <Card.Title>{Sender}</Card.Title>
        <div className='col-md-4'>
          <Image
            src={photo}
            roundedCircle
            fluid
            style={{
              height: 75,
              width: 75,
              background: "white",
            }}
          />
        </div>
        <div className='col-md-8'>
          <Card.Body>
            <Card text='muted' border='0'>
              <Card.Subtitle>{time}</Card.Subtitle>
            </Card>
            <Card.Text>{messageText}</Card.Text>
          </Card.Body>
        </div>
      </div>
    </Card>
  );
}

export default MessageBox;
