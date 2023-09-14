import React, { Component } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';

export class Home extends Component {
    static displayName = Home.name;
    render() {
        this.state = { open: false };

    return (
        <div>

            <h4>
                As the name goes <b>Make a guess</b>, you need to guess the answer to a question.<br /><br />
                But there is a catch <span>&#129300;</span>, you will get only 10 seconds<span>&#128561;</span>.<br /><br />
                Let's get started!<span>&#128077;</span>

                <br /><br /><br /><br />

            </h4>
            <label className="startButton"><Link to='/fetch-data'><b>Start</b></Link></label>
      </div>
    );
  }
}
