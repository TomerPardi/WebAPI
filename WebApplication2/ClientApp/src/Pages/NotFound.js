import React from "react";
import { Card } from "react-bootstrap";
import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
      }}
    >
      <Card className='shadow p-5'>
        <h1>
          {" "}
          404 <br />
          Page Not Found <br />
          <Link to='/'>Go home</Link>
        </h1>
      </Card>
    </div>
  );
}
