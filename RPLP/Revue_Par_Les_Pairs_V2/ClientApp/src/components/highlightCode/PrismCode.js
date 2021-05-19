import React from "react";
import Prism from "prismjs"

import "prismjs/plugins/line-numbers/prism-line-numbers.js"
import "prismjs/plugins/line-numbers/prism-line-numbers.css"

import "prismjs/themes/prism-okaidia.css"

export default class PrismCode extends React.Component {
  constructor(props) {
    super(props);
    this.ref = React.createRef();
  }

  componentDidMount() {
    this.highlight();
  }
  
  componentDidUpdate() {
    this.highlight();
  }

  highlight = () => {
    if (this.ref && this.ref.current) {
      Prism.highlightElement(this.ref.current);
    }
  };

  render() {
    const { code, plugins, language } = this.props;
    return (
      <pre className={!plugins ? "" : plugins.join(" ")}style={{ maxHeight: "55em", minHeight: "55em" }}>
        <code ref={this.ref} className={`language-${language}`}>
          {code.trim()}
        </code>
      </pre>
    );
  }
}
